export interface UserInfo {
    id: string;
    name: string;
    email: string;
    profilePictureUrl?: string; // Optional field for profile picture URL
    createdAt: Date; // Date when the user was created
    updatedAt: Date; // Date when the user was last updated
    }