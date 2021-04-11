import {UserInfoDto} from "../DAL/Dtos/UserInfoDto";
import {computed, makeObservable, observable} from "mobx";
import AuthenticationRepository from "../DAL/Repositories/AuthenticationRepository";
import {AuthenticateInfo} from "../ViewModels/Account/AuthenticateInfo";

export default class AuthenticationStore {
    public user: UserInfoDto | undefined;

    public constructor() {
        makeObservable(this, {
            user: observable,
            isAuthenticated: computed
        });
    }

    public static build = async () => {
        const instance = new AuthenticationStore();
        await instance.checkAuthentication();
        return instance;
    }

    public get isAuthenticated() : boolean {
        return this.user !== undefined;
    }

    public checkAuthentication = async () => {
        const resp = await AuthenticationRepository.CheckAuthentication();
        if(resp.success){
            this.user = resp.payload;
        }
    }

    public authenticate = async (login: string, password: string) => {
        const resp = await AuthenticationRepository.Authenticate({
            login: login,
            password: password
        } as AuthenticateInfo);
        if(resp.success){
            this.user = resp.payload;
        }
    }

    public logout = async () => {
        await AuthenticationRepository.Logout();
        this.user = undefined;
    }
}