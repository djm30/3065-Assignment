export interface Response extends Kind {
    error: boolean;
    modules: string[];
    marks: string[];
}

export enum Kinds {
    minMax,
    sort,
    marksForNext,
    total,
    classify,
    mean,
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

export interface TotalMarksResponse extends Response {
    total: number;
}

export interface ClassifyGradeResponse extends Response {
    grade: string;
}

export interface MarksForNextResponse extends Response {
    marks_required: Module[];
}

export interface MeanMarkResponse extends Response {
    mean: number;
}

export interface Module {
    module: string;
    marks: string;
}

export type ResponseTypes =
    | MinMaxResponse
    | SortedResponse
    | TotalMarksResponse
    | ClassifyGradeResponse
    | MarksForNextResponse
    | MeanMarkResponse;
