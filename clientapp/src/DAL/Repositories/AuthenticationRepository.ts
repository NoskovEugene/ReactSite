import {AuthenticateInfo} from "../../ViewModels/Account/AuthenticateInfo";
import Utils from "../../Core/Utils";
import {SuccessResponse} from "../../ViewModels/Responses/SuccessResponse";
import {UserInfoDto} from "../Dtos/UserInfoDto";
import {AxiosRequestConfig} from "axios";

export default class AuthenticationRepository {
    public static async Authenticate(authInfo: AuthenticateInfo): Promise<SuccessResponse<UserInfoDto>> {
        const resp = await Utils.fetch<SuccessResponse<UserInfoDto>>(
            '/api/v1/account/authenticate',
            true,
            "post",
            {
                data: authInfo
            } as AxiosRequestConfig);
        return resp.data;
    }

    public static async CheckAuthentication() : Promise<SuccessResponse<UserInfoDto>>{
        const resp = await Utils.fetch<SuccessResponse<UserInfoDto>>(
            '/api/v1/account/checkauth',
            true,
            "get",
        );
        return resp.data;
    }

    public static async Logout() : Promise<void> {
        const resp = await Utils.fetch('/api/v1/account/logout',
            true,
            'post');
    }
}