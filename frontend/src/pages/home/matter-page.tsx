import { MatterList } from "@/components/home-panel/matter"
import { CreateMatter } from "@/components/home-panel/matter/create-matter"
import { Badge } from "@/components/ui/badge"
import { useAppSelector } from "@/redux/hooks"
import { Helmet } from "react-helmet-async"

export function MatterPage() {
    const matters = useAppSelector(state => state.mattersReducer.matters)
    const countMatters = Object.keys(matters).length

    return (
        <>
            <Helmet title="Matérias" />
            <div className="space-y-8 divide-y">
                <div className="flex items-center justify-between">
                    <h1 className="font-bold text-lg text-primary">
                        Matérias &nbsp;&nbsp;
                        <Badge>{countMatters}</Badge>
                    </h1>
                    <CreateMatter />
                </div>
                <MatterList />
            </div>
        </>

    )
}
