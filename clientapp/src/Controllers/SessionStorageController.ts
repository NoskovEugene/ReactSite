export enum Constants {
    backendUrl,
    clientApp
}


export default class SessionStorageController {
    public static setItem(obj: string, key: Constants) {
        sessionStorage.setItem(Constants[key], obj);
    }

    public static setObject<T>(obj: T, key: Constants) {
        SessionStorageController.setItem(JSON.stringify(obj), key);
    }

    public static getObject<T>(key: Constants): T | undefined {
        const value = sessionStorage.getItem(key.toString());
        if (value !== "" && value !== null) {
            return JSON.parse(value!) as T;
        }
        return undefined;
    }

    public static clear() {
        sessionStorage.clear();
    }

    public static getItem(key: Constants): string | null {
        return sessionStorage.getItem(Constants[key]);
    }
}