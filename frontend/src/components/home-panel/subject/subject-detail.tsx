import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
} from "@/components/ui/sheet";
import api, { ResponseApi } from "@/lib/api";
import { cn, formatOrderReview } from "@/lib/utils";
import { Trash2 } from "lucide-react";
import { useEffect, useState } from "react";

interface SubjectDetailProps {
  subject: Subject | undefined;
  open: boolean;
  setOpen: (state: boolean) => void;
}

export function SubjectDetail({ subject, open, setOpen }: SubjectDetailProps) {
  const [contents, setContents] = useState<Content[]>([]);

  async function fetchSubjectDetail() {
    try {
      const { data } = await api.get<ResponseApi<Content[]>>(
        `/api/subject/${subject?.id}/contents`
      );
      if (data.data) setContents(data.data);
    } catch (e) {
      console.log(e);
    }
  }

  const isLastElement = (index: number) => {
    return contents.length - 1 === index;
  };

  useEffect(() => {
    if (subject) {
      fetchSubjectDetail();
    }
  }, [subject]);

  return (
    <Sheet open={open} onOpenChange={setOpen}>
      {subject && (
        <SheetContent className="w-[400px] md:min-w-[540px]">
          <SheetHeader>
            <SheetTitle className="text-primary">{subject.name}</SheetTitle>
            <SheetDescription className="sr-only">
              {subject.name}
            </SheetDescription>
          </SheetHeader>
          <ul role="list">
            {contents.map((content, index) => (
              <li
                key={content.id}
                className="rounded-xl px-4 flex w-full flex-col hover:bg-accent "
              >
                <div className="flex items-center justify-between gap-x-6 py-5">
                  <div className="min-w-0">
                    <div className="flex flex-col items-start gap-x-3">
                      <p className="text-base font-semibold leading-6">
                        {content.name}
                      </p>
                      <p className="text-sm leading-6">{content.note}</p>
                    </div>
                    <div className="mt-1 flex items-center gap-x-2 text-xs leading-5 text-muted-foreground">
                      <p className="whitespace-nowrap">
                        Você está na{" "}
                        {formatOrderReview(content.reviewNumber)
                          .charAt(0)
                          .toLocaleLowerCase()}
                        {formatOrderReview(content.reviewNumber).slice(1)}
                      </p>
                    </div>
                  </div>
                  <div className="flex flex-none items-center gap-x-4">
                    <Button size={"icon"} variant={"outline"}>
                      <Trash2 className="text-destructive" />
                    </Button>
                  </div>
                </div>

                <Separator
                  className={cn(isLastElement(index) ? "hidden" : "block")}
                />
              </li>
            ))}
          </ul>
        </SheetContent>
      )}
    </Sheet>
  );
}
