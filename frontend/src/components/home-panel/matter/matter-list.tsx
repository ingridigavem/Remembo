import { fetchMatters } from "@/redux/features/matters/thunks"
import { useAppDispatch, useAppSelector } from "@/redux/hooks"
import { Library } from "lucide-react"
import { useEffect } from "react"
import { CreateMatter } from "./create-matter"
import { RemoveMatter } from "./remove-matter"


export function MatterList() {
    const dispatch = useAppDispatch()
    const matters = useAppSelector(state => state.mattersReducer.matters)
    const countMatters = Object.keys(matters).length
    useEffect(() => {
        dispatch(fetchMatters())
    }, [])


    return (
        <ul className="space-y-2 pt-8">
            {
                countMatters == 0 && (
                    <div className="text-center">
                        <Library className="mx-auto h-12 w-12 text-muted-foreground" />
                        <h3 className="mt-2 text-sm font-semibold ">Nenhuma matéria</h3>
                        <p className="mt-1 text-sm text-muted-foreground">Comece criando uma matéria.</p>
                        <div className="mt-6">
                            <CreateMatter />
                        </div>
                    </div>
                )
            }
            {
                countMatters > 0 && Object.keys(matters).map(key => (
                    <li key={`matter-${key}`} className="rounded-xl border bg-card text-card-foreground shadow ">
                        <div className="p-6 flex items-center space-x-6" >
                            <div className="w-full flex items-center justify-between">
                                <div>
                                    <p>{matters[key].name}</p>
                                </div>
                                <div>
                                    <RemoveMatter matter={matters[key]} />
                                </div>
                            </div>
                        </div>
                    </li>
                ))
            }
        </ul>
    )
}
