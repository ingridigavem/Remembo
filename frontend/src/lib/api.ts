import { ToastProps } from '@/components/ui/toast';
import { toast } from '@/components/ui/use-toast';
import axios, { AxiosError, AxiosResponse } from 'axios';

export type ResponseApi<T = object> = {
    data: T | null;
    status: number;
    errors: string[];
    hasErrors: boolean;
    success: boolean;
    exceptionMessage: string | null;
    sucessMessage?: string | null;
}

const api = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
})

api.interceptors.request.use((config) => {
    const token = localStorage.getItem(process.env.REACT_APP_TOKEN_KEY ?? "");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    config.timeout = 30000;

    return config;
})

api.interceptors.response.use((response: AxiosResponse<ResponseApi>) => {
    return response;
}, (error: AxiosError<ResponseApi>) => {
    const toastConfig: ToastProps = { duration: 1500, variant: "destructive" }
    if (error.response) {
        if (error.response.status === 400) {
            const responseData = error.response.data.errors;

            if(error.response.data.hasErrors) {
                if (typeof(responseData) == 'string') {
                    toast({
                        ...toastConfig,
                        description: responseData
                    });
                } else {
                    responseData.forEach((s: string) => {
                        toast({
                            ...toastConfig,
                            description: s,
                            className: "mb-2"
                        });
                    });
                }
            }


        } else if (error.response.status === 401) {
            localStorage.clear();
            toast({
                ...toastConfig,
                description: 'Sua seção expirou'
            });
        }else if (error.response.status === 404) {
            toast({
                ...toastConfig,
                description: 'O recurso não foi encontrado'
            });
        } else if (error.response.status === 500) {
            toast({
                ...toastConfig,
                description: 'Ocorreu um erro inesperado.'
            });

        }


    } else if (error.code === "ECONNABORTED") {
        toast({
            ...toastConfig,
            description: 'Tempo limite de espera excedido. Refaça a operação.'
        });
    }

    return Promise.reject(error);
})


export default api;
