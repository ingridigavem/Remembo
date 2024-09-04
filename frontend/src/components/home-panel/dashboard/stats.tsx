import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

export function Stats() {
    return (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                        Total de reviões concluídas
                    </CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="text-2xl font-bold">20</div>
                </CardContent>
            </Card>
            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                        Total de conteúdos finalizados
                    </CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="text-2xl font-bold">5</div>
                </CardContent>
            </Card>
            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                        Total de conteúdos em aberto
                    </CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="text-2xl font-bold">10</div>
                </CardContent>
            </Card>

        </div>
    )
}
