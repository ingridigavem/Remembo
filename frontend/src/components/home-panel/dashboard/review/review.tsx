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
import { formatOrderReview } from "@/lib/utils"
import { View } from "lucide-react"

interface ReviewProps extends React.ButtonHTMLAttributes<HTMLButtonElement>{
    review: Review
}
export function Review({ review, ...props }: ReviewProps) {
    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button variant="ghost" size="icon">
                    <View className="text-primary" />
                </Button>
            </DialogTrigger>

            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>Revisão</DialogTitle>
                    <DialogDescription>
                        {formatOrderReview(review.reviewNumber)}
                    </DialogDescription>
                </DialogHeader>
                <div className="space-y-4">
                    <div>
                        <label className="font-bold leading-10">Matéria</label>
                        <p>Matéria</p>
                    </div>
                    <div>
                        <label className="font-bold leading-10">Conteúdo</label>
                        <p>{review.content}</p>
                    </div>
                    <div>
                        <label className="font-bold leading-10">Anotações</label>
                        <p>{review.note}</p>
                    </div>
                </div>
                <DialogFooter>
                    <Button className="w-full" {...props}>Marcar como feito</Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}
