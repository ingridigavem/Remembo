import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { toast } from "@/components/ui/use-toast";
import api, { ResponseApi } from "@/lib/api";
import { stringToDateFormatted } from "@/lib/date";
import { cn, formatOrderReview } from "@/lib/utils";
import { fetchDashboard } from "@/redux/features/dashboard/thunk";
import { useAppDispatch } from "@/redux/hooks";
import { BookOpenCheck } from "lucide-react";
import { useState } from "react";
import { Review } from "../review";

interface ReviewListProps {
  subjects: SubjectWithContent[];
}

export function SubjectContentReviewList({ subjects }: ReviewListProps) {
  const dispatch = useAppDispatch();
  const [isLoading, setIsLoading] = useState(false);

  const handleMarkDone = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    id: string
  ) => {
    try {
      e.stopPropagation();
      setIsLoading(true);
      let descriptionMessage = "";
      const { data } = await api.post<ResponseApi<Review>>(`/api/review/${id}`);
      if (data.data == null) descriptionMessage = data.sucessMessage ?? "";
      else
        descriptionMessage = `A sua próxima revisão deste conteúdo será dia ${stringToDateFormatted(
          data.data.scheduleReviewDate
        )}`;

      await dispatch(fetchDashboard());

      setIsLoading(false);
      toast({
        variant: "success",
        title: "Parabéns! Mais um conteúdo revisado",
        description: descriptionMessage,
        duration: 1500,
      });
    } catch (error) {
      console.log(error);
      setIsLoading(false);
    }
  };

  const isLastElement = (index: number) => {
    return subjects.filter((m) => m.contents.length > 0).length - 1 === index;
  };

  return (
    <ul className="space-y-8">
      {subjects
        .filter((m) => m.contents.length > 0)
        .map((subject, index) => (
          <li key={`subject-${subject.subjectId}`} className="space-y-4">
            <h2 className="font-bold">{subject.subjectName}</h2>
            <ul
              className={cn(
                "space-y-4",
                !isLastElement(index) ? "pb-4" : "pb-0"
              )}
            >
              {subject.contents.map((content) => (
                <li
                  key={`content-review-${content.contentName}`}
                  className="rounded-xl border bg-card text-card-foreground shadow "
                >
                  <div className="p-6 flex items-center space-x-6">
                    <Button
                      size="icon"
                      onClick={(e) =>
                        handleMarkDone(e, content.currentReview.reviewId)
                      }
                    >
                      <BookOpenCheck className="text-primary-foreground" />
                    </Button>
                    <div className="w-full flex justify-between">
                      <div>
                        <p>{content.contentName}</p>
                        <p className="text-muted-foreground text-sm">
                          {formatOrderReview(content.reviewNumber)}{" "}
                        </p>
                      </div>
                      <Review
                        contentReview={content}
                        subjectName={subject.subjectName}
                        onClick={(e) =>
                          handleMarkDone(e, content.currentReview.reviewId)
                        }
                        isLoading={isLoading}
                      />
                    </div>
                  </div>
                </li>
              ))}
            </ul>

            <Separator
              className={cn(isLastElement(index) ? "hidden" : "block")}
            />
          </li>
        ))}
    </ul>
  );
}
