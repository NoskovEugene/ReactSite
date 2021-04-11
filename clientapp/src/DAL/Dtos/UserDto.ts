import {EntityBase} from "../Models/EntityBase";

export interface User extends EntityBase {
    email: string,
    login: string,
    passwordHash: string,
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