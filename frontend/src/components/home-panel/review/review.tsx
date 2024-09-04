import { Button } from "@/components/ui/button"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog"
import { Icons } from "@/components/ui/icons"
import { formatOrderReview } from "@/lib/utils"
import { View } from "lucide-react"
import { useState } from "react"

interface ReviewProps extends React.ButtonHTMLAttributes<HTMLButtonElement>{
    contentReview: ContentReview,
    matterName: string,
    isLoading: boolean,
}
export function Review({ contentReview, matterName, isLoading, ...props }: ReviewProps) {
    const [ open, setOpen ] = useState(false)

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button variant="ghost" size="icon">
                    <View className="text-primary" />
                </Button>
            </DialogTrigger>

            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>Revisão</DialogTitle>
                    <DialogDescription>
                        {formatOrderReview(contentReview.reviewNumber)}
                    </DialogDescription>
                </DialogHeader>
                <div className="space-y-4">
                    <div>
                        <label className="font-bold leading-10">Matéria</label>
                        <p>{matterName}</p>
                    </div>
                    <div>
                        <label className="font-bold leading-10">Conteúdo</label>
                        <p>{contentReview.contentName}</p>
                    </div>
                    <div>
                        <label className="font-bold leading-10">Anotações</label>
                        <p>{contentReview.note}</p>
                    </div>
                </div>
                <DialogFooter>
                    <Button disabled={isLoading} className="w-full" {...props}>
                        {isLoading && (
                            <Icons.spinner className="mr-2 h-4 w-4 animate-spin" />
                        )}
                        Marcar como feito
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}
