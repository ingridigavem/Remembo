import {
    ColumnDef,
    ColumnFiltersState,
    TableMeta,
    flexRender,
    getCoreRowModel,
    getFilteredRowModel,
    useReactTable
} from "@tanstack/react-table"

import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"

import { Input } from "@/components/ui/input"
import { Search } from "lucide-react"
import { useState } from "react"

interface TEntity {
    id: string
}

interface DataTableProps<TData extends TEntity, TValue> {
    columns: ColumnDef<TData, TValue>[]
    data: TData[],
    meta ?: TableMeta<TData> | undefined
    rightElement?: React.ReactNode
}

export function DataTable<TData extends TEntity, TValue>({
    columns,
    data,
    meta,
    rightElement
}: DataTableProps<TData, TValue>) {
    const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([])

    const table = useReactTable({
        data,
        columns,
        getRowId: originalRow => originalRow.id,
        getCoreRowModel: getCoreRowModel(),
        onColumnFiltersChange: setColumnFilters,
        getFilteredRowModel: getFilteredRowModel(),
        state: {
          columnFilters,
        },
        meta: meta,
    })

    return (
        <div>
            <div className="flex items-center justify-between py-4">
                <div className="relative w-auto max-w-sm">
                    <Input
                        placeholder="Pesquisar..."
                        value={(table.getColumn("descricao")?.getFilterValue() as string) ?? ""}
                        onChange={(event) =>
                            table.getColumn("descricao")?.setFilterValue(event.target.value)
                        }
                        className="pr-8"
                    />
                    <Search className="absolute right-2 top-2.5 h-4 w-4 text-muted-foreground" />
                </div>
                <div>
                    {rightElement}
                </div>
            </div>
            <div className="rounded-md border">
                <Table>
                    <TableHeader>
                        {table.getHeaderGroups().map((headerGroup) => (
                            <TableRow key={headerGroup.id}>
                            {headerGroup.headers.map((header) => {
                                return (
                                <TableHead key={header.id}>
                                    {header.isPlaceholder
                                    ? null
                                    : flexRender(
                                        header.column.columnDef.header,
                                        header.getContext()
                                        )}
                                </TableHead>
                                )
                            })}
                            </TableRow>
                        ))}
                    </TableHeader>
                    <TableBody>
                        {table.getRowModel().rows?.length ? (
                            table.getRowModel().rows.map((row) => (
                                <TableRow
                                    key={row.id}
                                    data-state={row.getIsSelected() && "selected"}
                                >
                                    {row.getVisibleCells().map((cell) => (
                                        <TableCell key={cell.id}>
                                            {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                        </TableCell>
                                    ))}
                                </TableRow>
                            ))
                        ) : (
                            <TableRow>
                                <TableCell colSpan={columns.length} className="h-24 text-center">
                                    Sem resultados.
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </div>
        </div>
    )
}
