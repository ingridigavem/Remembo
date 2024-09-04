import { Stats } from "@/components/home-panel/dashboard/stats";
import { MatterContentReviewList } from "@/components/home-panel/matter/matter-contentdetail-list";
import { Badge } from "@/components/ui/badge";
import { selectCountContentsOnComming, selectCountContentsOverdue, selectMatterContentsOnComming, selectMatterContentsOverdue } from "@/redux/features/dashboard/dashboardSlice";
import { fetchDashboard } from "@/redux/features/dashboard/thunk";
import { fetchMatters } from "@/redux/features/matters/thunks";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { useEffect } from "react";
import { Helmet } from "react-helmet-async";


export function HomePage() {
    const dispatch = useAppDispatch()
    const mattersContentReviewsOnComming = useAppSelector(selectMatterContentsOnComming)
    const mattersContentReviewsOverdue = useAppSelector(selectMatterContentsOverdue)
    const countContentsOnComming = useAppSelector(selectCountContentsOnComming)
    const countContentsOverdue = useAppSelector(selectCountContentsOverdue)

    useEffect(() => {
        dispatch(fetchDashboard())
        dispatch(fetchMatters())
    }, [])

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
                    <MatterContentReviewList matters={mattersContentReviewsOnComming} />
                </div>

                <div className="space-y-4">
                    <h1 className="font-bold text-lg text-destructive">
                        Revisões vencidas&nbsp;&nbsp;
                        <Badge variant="destructive">{countContentsOverdue}</Badge>
                    </h1>
                    <MatterContentReviewList matters={mattersContentReviewsOverdue}/>
                </div>
            </div>
        </>

    )
}
