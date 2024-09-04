import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { toast } from "@/components/ui/use-toast";
import { cn, formatOrderReview } from "@/lib/utils";
import { BookOpenCheck } from "lucide-react";
import { Review } from "../review";


interface ReviewListProps {
    matters: MatterWithContent[]
}

export function MatterContentReviewList({ matters } : ReviewListProps) {
    const handleMarkDone = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>, id: string) => {
        e.stopPropagation();
        toast({
            variant: "success",
            title: "Parabéns!!",
            description: "Mussum Ipsum, cacilds vidis litro abertis. Mauris nec dolor in eros commodo tempor.",
            duration: 1500
        })
    }

    const isLastElement = (index: number) => {
        return matters.filter(m => m.contents.length > 0).length - 1 === index
    }

    return (
        <ul className="space-y-8">
            {
                matters.filter(m => m.contents.length > 0).map((matter, index) => (
                    <li key={`matter-${matter.matterId}`} className="space-y-4" >
                        <h2 className="font-bold">{matter.matterName}</h2>
                        <ul className={cn("space-y-4", !isLastElement(index) ? "pb-4" : "pb-0")}>
                            {
                                matter.contents.map(content => (
                                    <li key={`content-review-${content.contentName}`} className="rounded-xl border bg-card text-card-foreground shadow ">
                                        <div className="p-6 flex items-center space-x-6" >
                                            <Button
                                                size="icon" onClick={(e) => handleMarkDone(e, content.currentReview.reviewId)}
                                            >
                                                <BookOpenCheck className="text-primary-foreground" />
                                            </Button>
                                            <div className="w-full flex justify-between">
                                                <div>
                                                    <p>{content.contentName}</p>
                                                    <p className="text-muted-foreground text-sm">{formatOrderReview(content.reviewNumber)} </p>
                                                </div>
                                                <Review contentReview={content} onClick={(e) => handleMarkDone(e, content.currentReview.reviewId)} />
                                            </div>
                                        </div>
                                    </li>
                                ))
                            }
                        </ul>

                        <Separator className={cn( isLastElement(index) ? "hidden" : "block" )}
                        />
                    </li>
                ))
            }
        </ul>
    )
}
