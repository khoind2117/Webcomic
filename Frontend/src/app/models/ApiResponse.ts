export interface ApiResponse<T> {
    totalCount: number;
    pageNumber: number;
    totalPages: number;
    items: T[];
}