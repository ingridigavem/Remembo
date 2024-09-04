import { UserNav } from "@/components/home-panel/user-nav";
import { ModeToggle } from "@/components/mode-toggle";
import { cn } from "@/lib/utils";
import { useLocation } from "react-router-dom";

const navigation = [
    { name: 'Painel', href: '/', current: true },
    { name: 'Matérias', href: '/materias', current: false },
]

export function Navbar() {
    const location = useLocation();

    return (
        <nav className="sticky flex justify-between border-b top-0 z-10 w-full bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 ">
            <div className="max-w-7xl px-4 sm:px-6 lg:px-8">
                <div className="flex h-16 justify-between">
                    <div className="flex">
                        <div className="flex flex-shrink-0 items-center">
                            <img
                                alt="Remembo Icon"
                                src="/logo128.png"
                                className="block h-8 w-auto"
                            />
                        </div>
                        <div className="hidden sm:-my-px sm:ml-6 sm:flex sm:space-x-8">
                            {navigation.map((item) => (
                                <a
                                    key={item.name}
                                    href={item.href}
                                    aria-current={location.pathname === item.href ? 'page' : undefined}
                                    className={cn(
                                        location.pathname === item.href
                                        ? 'border-primary text-foreground'
                                        : 'border-transparent text-muted-foreground hover:border-gray-300 hover:text-accent-foreground',
                                        'inline-flex items-center border-b-2 px-1 pt-1 text-sm font-medium',
                                    )}
                                >
                                    {item.name}
                                </a>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
            <div className="mx-4 sm:mx-8 flex h-14 items-center">
                <div className="flex flex-1 items-center space-x-2 justify-end">
                    <ModeToggle />
                    <UserNav />
                </div>
            </div>
        </nav>
    );
}
