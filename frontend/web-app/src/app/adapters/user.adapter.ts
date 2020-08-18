import { Adapter } from './adapter';
import { User } from '../models/user.model';

export class UserAdapter extends Adapter<User> {

    adapt(raw: any): User {
        return raw;
    }

}
