export interface Application {
    id: number;
    description: string;
    entryDate: string;
    resolutionDate: string;
    isSolved: boolean
}

export interface State{
    applications: Application[];
}