import { MatterList } from "@/components/home-panel/matter"
import { CreateMatter } from "@/components/home-panel/matter/create-matter"
import { Badge } from "@/components/ui/badge"
import { useState } from "react"

export function MatterPage() {
    const [ matters, setMatters ] = useState<Matter[]>([{
        id: "1",
        name: "Português"
    }])

    const countMatters = matters.length

    return (
        <div className="space-y-8">
            <div className="flex items-center justify-between">
                <h1 className="font-bold text-lg text-primary">
                    Matérias &nbsp;&nbsp;
                    <Badge>{countMatters}</Badge>
                </h1>
                <CreateMatter />
            </div>
            <MatterList matters={matters} />
        </div>
    )
}
