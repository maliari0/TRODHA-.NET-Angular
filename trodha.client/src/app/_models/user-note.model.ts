// trodha.client/src/app/_models/user-note.model.ts
export interface UserNote {
  noteId: number;
  userId: number;
  content: string;
  createdAt: Date;
  updatedAt: Date;
  images: NoteImage[];
}

export interface CreateUserNote {
  content: string;
}

export interface UpdateUserNote {
  content: string;
}

export interface NoteImage {
  imageId: number;
  noteId: number;
  imagePath: string;
  imageType: string;
  createdAt: Date;
  imageUrl: string;
}
