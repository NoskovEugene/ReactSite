export interface UserInfoDto {
    email: string,
    login: string,
    /** Фамилия **/
    lastName: string,
    /** Имя **/
    firstName: string,
    /** Отчество **/
    middleName: string,
    /** Пол **/
    gender: string
    age: number
}