export interface Response extends Kind {
    error: boolean;
    modules: string[];
    marks: string[];
}

export enum Kinds {
    minMax,
    sort,
}

export interface Kind {
    kind: Kinds;
}

export interface MinMaxResponse extends Response {
    max_module: string;
    min_module: string;
}

export interface SortedResponse extends Response {
    sorted_modules: Module[];
}

export interface Module {
    module: string;
    marks: string;
}
