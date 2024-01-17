export interface SearchResponse<TRecordModel> {
	// RECORDS
	records: TRecordModel[];
	// TOTAL COUNT
	totalCount: number;
}
