import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import SessionStorageController, {Constants} from "../Controllers/SessionStorageController";

export default class Utils {


    public static GetBackendUrlFromStorage(): string {
        return  SessionStorageController.getItem(Constants.backendUrl)!;
    }

    public static async fetch<T>(url: string,
                                 isRelated: boolean,
                                 method: "get" | "GET" | "post" | "POST",
                                 init?: AxiosRequestConfig): Promise<AxiosResponse<T>> {
        const config : AxiosRequestConfig = {
            headers : {
                "Content-Type" : "application/json"
            },
            withCredentials: true,
            ...init,
            url: isRelated ? `${this.GetBackendUrlFromStorage()}${url}` : url,
            method : method
        }
        return await axios.request<T>(config);
    }
}