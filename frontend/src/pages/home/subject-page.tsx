import { SubjectList } from "@/components/home-panel/subject";
import { CreateSubject } from "@/components/home-panel/subject/create-subject";
import { Badge } from "@/components/ui/badge";
import { useAppSelector } from "@/redux/hooks";
import { Helmet } from "react-helmet-async";

export function SubjectPage() {
  const subjects = useAppSelector((state) => state.subjectsReducer.subjects);
  const countSubjects = Object.keys(subjects).length;

  return (
    <>
      <Helmet title="Matérias" />
      <div className="space-y-8 divide-y">
        <div className="flex items-center justify-between">
          <h1 className="font-bold text-lg text-primary">
            Matérias &nbsp;&nbsp;
            <Badge>{countSubjects}</Badge>
          </h1>
          <CreateSubject />
        </div>
        <SubjectList />
      </div>
    </>
  );
}
