import {makeObservable, observable} from "mobx";

export default class SettingsStore{
    public applicationUrl: string;

    public backendUrl: string;

    public constructor() {
        this.applicationUrl = "";
        this.backendUrl = "";
        makeObservable(this, {
            applicationUrl: observable,
            backendUrl: observable
        });
    }

    public static build = async () => {
        const instance = new SettingsStore();
        return instance;
    }
}