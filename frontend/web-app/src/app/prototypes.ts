// DATE
Date.prototype.addDays = addDays;
Date.prototype.addMinutes = addMinutes;
Date.prototype.getPrettyString = getPrettyString;
Date.prototype.equals = dateEquals;
Date.prototype.difference = dateDifference;

interface Date {
    addDays: typeof addDays;
    addMinutes: typeof addMinutes;
    getPrettyString: typeof getPrettyString;
    equals: typeof dateEquals;
    difference: typeof dateDifference;
}

function dateEquals(date2: Date): boolean {
    return this.getFullYear() === date2.getFullYear() &&
        this.getMonth() === date2.getMonth() &&
        this.getDate() === date2.getDate();
}

function addDays(days: number): Date {
    const date = new Date(this);
    date.setDate(this.getDate() + days);
    return date;
}

function addMinutes(minutes: number): Date {
    const date = new Date(this);
    date.setMinutes(this.getMinutes() + minutes);
    return date;
}

function dateDifference(date2: Date, divider: number = 1000 * 60 * 60 * 24): number {
    const utc1 = Date.UTC(this.getFullYear(), this.getMonth(), this.getDate(),
        this.getHours(), this.getMinutes(), this.getSeconds(), this.getMilliseconds());
    const utc2 = Date.UTC(date2.getFullYear(), date2.getMonth(), date2.getDate(),
        date2.getHours(), date2.getMinutes(), date2.getSeconds(), date2.getMilliseconds());
    return Math.floor((utc2 - utc1) / divider);
}

function getPrettyString(): string {
    const prettyDay = ('0' + this.getDate()).slice(-2);
    const prettyMonth = ('0' + (this.getMonth() + 1)).slice(-2);
    const prettyHour = ('0' + this.getHours()).slice(-2);
    const prettyMinutes = ('0' + this.getMinutes()).slice(-2);
    return `${prettyDay}.${prettyMonth}.${this.getFullYear()} ${prettyHour}:${prettyMinutes}`;
}

// STRING
String.prototype.countCharacter = countCharacter;
String.prototype.equals = stringEquals;

interface String {
    countCharacter: typeof countCharacter;
    equals: typeof stringEquals;
}

function stringEquals(string2: string): boolean {
    return this && string2 ? string2.toLowerCase().trim() === this.toLowerCase().trim() : false;
}

function countCharacter(findChar: string): number {
    let result = 0;
    for (const char of this) {
        if (char === findChar) {
            result += 1;
        }
    }
    return result;
}
