import { AuditableDto } from "../AuditableDto";

export interface IUserDto extends AuditableDto {
    emailAddress?: string;
    firstName?: string;
    isActive?: boolean
    imagePath? : string
    lastLoginDate?: Date | null;
    lastName?: string;
}