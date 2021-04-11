import {makeObservable, observable} from "mobx";

export default class MetadataStore {
    public title: string;

    public constructor() {
        this.title = "React client app";
        makeObservable(this, {
            title: observable
        });
    }

}