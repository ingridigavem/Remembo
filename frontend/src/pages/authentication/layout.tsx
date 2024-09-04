import { login, selectUser } from "@/redux/features/user/userSlice";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";

export function LayoutAuthentication() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const user = useAppSelector(selectUser)

    useEffect(() => {
        const token = localStorage.getItem(process.env.REACT_APP_TOKEN_KEY ?? "")
        console.log(token, user)
        if(!user && token) {
            dispatch(login(token))
            navigate("/")
        }
    }, [])

    return (
        <div className="flex min-h-full flex-1 flex-col justify-center py-12 sm:px-6 lg:px-8">
            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-[480px]">
                <div className="px-6 py-12 sm:rounded-lg sm:px-12">
                    <Outlet />
                </div>
            </div>
        </div>
    )
}
