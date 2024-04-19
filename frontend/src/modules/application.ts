export interface Application {
    id: number;
    description: string;
    entryDate: Date;
    resolutionDate: Date;
    isSolved: boolean
}

export interface State{
    applications: Application[];
}