import { Button } from "@/components/ui/button";

export default function NotFoundPage() {
    return (
        <main className="grid h-screen place-items-center px-6 py-24 sm:py-32 lg:px-8">
            <div className="text-center">
                <p className="text-base font-semibold text-foreground">404</p>
                <h1 className="mt-4 text-3xl font-bold tracking-tight text-foreground sm:text-5xl">Página não encontrada</h1>
                <p className="mt-6 text-base leading-7 text-muted-foreground">Não conseguimos encontrar a página que procura.</p>
                <div className="mt-10 flex items-center justify-center gap-x-6">
                    <Button
                        className="flex justify-center items-center pb-2 pt-1"
                        variant={"link"}
                        asChild
                    >
                        <a href="/" className="flex items-center gap-2">
                            <span aria-hidden="true">&larr;</span>
                            Voltar para a página inicial
                        </a>
                    </Button>
                </div>
            </div>
        </main>
    )
}
