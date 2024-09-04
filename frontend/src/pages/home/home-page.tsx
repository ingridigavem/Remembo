import { Stats } from "@/components/home-panel/dashboard/stats";
import { SubjectContentReviewList } from "@/components/home-panel/subject/subject-contentdetail-list";
import { Badge } from "@/components/ui/badge";
import {
  selectCountContentsOnComming,
  selectCountContentsOverdue,
  selectSubjectContentsOnComming,
  selectSubjectContentsOverdue,
} from "@/redux/features/dashboard/dashboardSlice";
import { fetchDashboard } from "@/redux/features/dashboard/thunk";
import { fetchSubjects } from "@/redux/features/subjects/thunks";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { useEffect } from "react";
import { Helmet } from "react-helmet-async";

export function HomePage() {
  const dispatch = useAppDispatch();
  const subjectsContentReviewsOnComming = useAppSelector(
    selectSubjectContentsOnComming
  );
  const subjectsContentReviewsOverdue = useAppSelector(
    selectSubjectContentsOverdue
  );
  const countContentsOnComming = useAppSelector(selectCountContentsOnComming);
  const countContentsOverdue = useAppSelector(selectCountContentsOverdue);

  useEffect(() => {
    dispatch(fetchDashboard());
    dispatch(fetchSubjects());
  }, []);

  return (
    <>
      <Helmet title="Início" />
      <div className="space-y-8">
        <Stats />

        <div className="space-y-4">
          <h1 className="font-bold text-lg text-primary">
            Revisões de hoje&nbsp;&nbsp;
            <Badge>{countContentsOnComming}</Badge>
          </h1>
          <SubjectContentReviewList
            subjects={subjectsContentReviewsOnComming}
          />
        </div>

        <div className="space-y-4">
          <h1 className="font-bold text-lg text-destructive">
            Revisões vencidas&nbsp;&nbsp;
            <Badge variant="destructive">{countContentsOverdue}</Badge>
          </h1>
          <SubjectContentReviewList subjects={subjectsContentReviewsOverdue} />
        </div>
      </div>
    </>
  );
}
