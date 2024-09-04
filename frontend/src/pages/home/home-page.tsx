import { Stats } from "@/components/home-panel/dashboard/stats";
import { ReviewList } from "@/components/home-panel/review";
import { Badge } from "@/components/ui/badge";
import { fetchDashboard } from "@/redux/features/dashboard/thunk";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { useEffect } from "react";
import { Helmet } from "react-helmet-async";


const today = new Date();
const yesterday = new Date();
yesterday.setDate(yesterday.getDate() - 1)

const reviews: Review[] = [
    {
        id: "1",
        content: "Mussum Ipsum, cacilds vidis litro abertis. Mauris nec dolor in eros commodo tempor.",
        isReviewed: false,
        reviewNumber: 1,
        scheduleReviewDate: today,
        note: "Mussum Ipsum, cacilds vidis litro abertis. Admodum accumsan disputationi eu sit. Vide electram sadipscing et per. Per aumento de cachacis, eu reclamis. Paisis, filhis, espiritis santis. Cevadis im ampola pa arma uma pindureta.",
    },
    {
        id: "2",
        content: "Mussum Ipsum, cacilds vidis litro abertis.",
        isReviewed: false,
        reviewNumber: 1,
        scheduleReviewDate: yesterday,
        note: "Mussum Ipsum, cacilds vidis litro abertis. Admodum accumsan disputationi eu sit. Vide electram sadipscing et per. Per aumento de cachacis, eu reclamis. Paisis, filhis, espiritis santis. Cevadis im ampola pa arma uma pindureta."
    }
]

export function HomePage() {
    const dispatch = useAppDispatch()
    const { matters } = useAppSelector(state => state.dashboardReducer)
    const reviewsOnComming = reviews.filter(r => r.scheduleReviewDate >= today)
    const countReviewsOnComming = reviewsOnComming.length

    console.log(matters)

    const reviewsOverdue = reviews.filter(r => today > r.scheduleReviewDate )
    const countReviewsOverdue = reviewsOverdue.length

    useEffect(() => {
        dispatch(fetchDashboard())
    }, [])

    return (
        <>
            <Helmet title="Início" />
            <div className="space-y-8">
                <Stats />

                <div className="space-y-4">
                    <h1 className="font-bold text-lg text-primary">
                        Revisões de hoje&nbsp;&nbsp;
                        <Badge>{countReviewsOnComming}</Badge>
                    </h1>
                    <ReviewList reviews={reviewsOnComming} />
                </div>

                <div className="space-y-4">
                    <h1 className="font-bold text-lg text-destructive">
                        Revisões vencidas&nbsp;&nbsp;
                        <Badge variant="destructive">{countReviewsOverdue}</Badge>
                    </h1>
                    <ReviewList reviews={reviewsOverdue} />
                </div>
            </div>
        </>

    )
}
