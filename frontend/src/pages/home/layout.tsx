import { CreateReview } from "@/components/home-panel/dashboard/create-review";
import { Navbar } from "@/components/home-panel/navbar";
import { Outlet } from "react-router-dom";

export function LayoutHome() {
    return (
        <div className="min-h-full relative">
            <CreateReview />
            <Navbar />
            <div className="lg:py-10">
                <main>
                    <div className="relative mx-auto max-w-7xl px-8 py-8 sm:px-6 lg:px-8">
                        <Outlet />
                    </div>
                </main>
            </div>
        </div>
    )
}
