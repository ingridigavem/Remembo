import { fetchMatters } from "@/redux/features/matters/thunks"
import { useAppDispatch, useAppSelector } from "@/redux/hooks"
import { Library } from "lucide-react"
import { useEffect, useState } from "react"
import { CreateMatter } from "./create-matter"
import { MatterDetail } from "./matter-detail"
import { RemoveMatter } from "./remove-matter"


export function MatterList() {
    const dispatch = useAppDispatch()
    const matters = useAppSelector(state => state.mattersReducer.matters)
    const countMatters = Object.keys(matters).length
    const [ open, setOpen ] = useState(false)
    const [ selectedMatter, setSelectedMatter ] = useState<Matter | undefined>()

    const selectMatter = (matter: Matter) => {
        setSelectedMatter(matter)
        setOpen(true)
    }

    useEffect(() => {
        dispatch(fetchMatters())
    }, [])

    return (
        <>
            <MatterDetail matter={selectedMatter} open={open} setOpen={setOpen} />

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
                        <li key={`matter-${key}`} className="relative rounded-xl border bg-card text-card-foreground shadow hover:cursor-pointer hover:bg-accent" >
                            <div className="p-6 flex items-center space-x-6" onClick={() => selectMatter(matters[key])}>
                                <div className="w-full flex items-center justify-between">
                                    <div>
                                        <p>{matters[key].name}</p>
                                    </div>

                                </div>
                            </div>

                            <div className="z-10 absolute right-10 top-6">
                                <RemoveMatter matter={matters[key]} />
                            </div>
                        </li>
                    ))
                }
            </ul>
        </>
    )
}
