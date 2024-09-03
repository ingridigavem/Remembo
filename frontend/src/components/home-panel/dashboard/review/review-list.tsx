import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";
import { formatOrderReview } from "@/lib/utils";
import { BookOpenCheck } from "lucide-react";
import { Review } from "./review";


interface ReviewListProps {
    reviews: Review[]
}

export default function ReviewList({ reviews } : ReviewListProps) {
    const { toast } = useToast();

    const handleMarkDone = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>, id: string) => {
        e.stopPropagation();
        toast({
            variant: "success",
            title: "Parabéns!!",
            description: "Mussum Ipsum, cacilds vidis litro abertis. Mauris nec dolor in eros commodo tempor.",
            duration: 1500
        })
    }

    return (
        <ul className="space-y-2">
            {
                reviews.map(review => (
                    <li key={`review-${review.id}`} className="rounded-xl border bg-card text-card-foreground shadow ">
                        <div className="p-6 flex items-center space-x-6" >
                            <Button
                                size="icon" onClick={(e) => handleMarkDone(e, review.id)}
                            >
                                <BookOpenCheck className="text-primary-foreground" />
                            </Button>
                            <div className="w-full flex justify-between">
                                <div>
                                    <p>{review.content}</p>
                                    <p className="text-muted-foreground text-sm">{formatOrderReview(review.reviewNumber)} </p>
                                </div>
                                <Review review={review} onClick={(e) => handleMarkDone(e, review.id)} />
                            </div>
                        </div>
                    </li>
                ))
            }
        </ul>
    )
}
