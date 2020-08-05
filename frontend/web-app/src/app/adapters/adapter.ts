export abstract class Adapter<T> {

    public abstract adapt(raw: any): T;

    public adaptMany(raws: any[]): T[] {
        const objs: T[] = [];
        raws.forEach(obj => objs.push(this.adapt(obj)));
        return objs;
    }

}
