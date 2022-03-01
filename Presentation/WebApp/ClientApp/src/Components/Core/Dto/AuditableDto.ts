export interface AuditableDto {
    id?: string;
    createdById?: string | null;
    createdOn?: Date | null;
    deletedById?: string | null;
    deletedOn?: Date | null;
    updatedById?: string | null;
    updatedOn?: Date | null;
}