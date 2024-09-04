import { LayoutGrid, Library, LogOut, User } from "lucide-react";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuGroup,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger
} from "@/components/ui/tooltip";
import { getAcronym } from "@/lib/string";
import { resetDashboard } from "@/redux/features/dashboard/dashboardSlice";
import { resetMatters } from "@/redux/features/matters/mattersSlice";
import { logout, selectUser } from "@/redux/features/user/userSlice";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { Link, useNavigate } from "react-router-dom";

export function UserNav() {
    const user = useAppSelector(selectUser)
    const dispatch = useAppDispatch()
    const navigate = useNavigate()

    const handleLogout = () => {
        dispatch(resetMatters())
        dispatch(resetDashboard())
        dispatch(logout())
        navigate("/entrar")
    }

    return (
        <DropdownMenu>
            <TooltipProvider disableHoverableContent>
                <Tooltip delayDuration={100}>
                    <TooltipTrigger asChild>
                        <DropdownMenuTrigger asChild>
                        <Button
                            variant="outline"
                            className="relative h-8 w-8 rounded-full"
                        >
                            <Avatar className="h-8 w-8">
                            <AvatarImage src="#" alt="Avatar" />
                            <AvatarFallback className="bg-transparent">{getAcronym(user?.name ?? "")}</AvatarFallback>
                            </Avatar>
                        </Button>
                        </DropdownMenuTrigger>
                    </TooltipTrigger>
                    <TooltipContent side="bottom">Perfil</TooltipContent>
                </Tooltip>
            </TooltipProvider>

            <DropdownMenuContent className="w-56" align="end" forceMount>
                <DropdownMenuLabel className="font-normal">
                    <div className="flex flex-col space-y-1">
                        <p className="text-sm font-medium leading-none">{user?.name}</p>
                        <p className="text-xs leading-none text-muted-foreground">
                            {user?.email}
                        </p>
                    </div>
                </DropdownMenuLabel>
                <DropdownMenuSeparator />
                <DropdownMenuGroup>
                    <DropdownMenuItem className="hover:cursor-pointer" asChild>
                        <Link to="/" className="flex items-center">
                            <LayoutGrid className="w-4 h-4 mr-3 text-muted-foreground" />
                            Painel
                        </Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem className="hover:cursor-pointer" asChild>
                        <Link to="/materias" className="flex items-center">
                            <Library className="w-4 h-4 mr-3 text-muted-foreground" />
                            Matérias
                        </Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem className="hover:cursor-pointer" asChild>
                        <Link to="/perfil" className="flex items-center">
                            <User className="w-4 h-4 mr-3 text-muted-foreground" />
                            Perfil
                        </Link>
                    </DropdownMenuItem>
                </DropdownMenuGroup>
                <DropdownMenuSeparator />
                <DropdownMenuItem className="hover:cursor-pointer" onClick={() => handleLogout()}>
                    <LogOut className="w-4 h-4 mr-3 text-muted-foreground" />
                    Sair
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    );
}
