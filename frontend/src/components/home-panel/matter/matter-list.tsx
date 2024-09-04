import { RemoveMatter } from "./remove-matter"

interface MatterListProps {
    matters: Matter[]
}
export function MatterList({ matters }: MatterListProps) {
    return (
        <ul className="space-y-2">
            {
                matters.map(matter => (
                    <li key={`matter-${matter.id}`} className="rounded-xl border bg-card text-card-foreground shadow ">
                        <div className="p-6 flex items-center space-x-6" >
                            <div className="w-full flex items-center justify-between">
                                <div>
                                    <p>{matter.name}</p>
                                </div>
                                <div>
                                    <RemoveMatter matter={matter} />
                                </div>
                            </div>
                        </div>
                    </li>
                ))
            }
        </ul>
    )
}
