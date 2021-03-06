export interface User {

    /** Primary key */
    id: number;

    /** Max length: 20, non-nullable */
    login: string;

    /** Max length: 254, non-nullable */
    password: string;

    /** Max length: 35, non-nullable */
    firstName: string;

    /** Max length: 35, non-nullable */
    lastName: string;

    /** Max length: 254, non-nullable */
    email: string;

}
