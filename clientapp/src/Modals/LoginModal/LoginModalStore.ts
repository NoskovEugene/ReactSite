import {makeObservable, observable} from "mobx";

export default class LoginModalStore{
    public passwordVisibility: boolean;

    public isOpen: boolean;

    public login: string;

    public password: string;

    public constructor() {
        this.passwordVisibility = false;
        this.isOpen = false;
        this.login = "";
        this.password = "";
        makeObservable(this, {
            passwordVisibility: observable,
            isOpen: observable
        });
    }
}