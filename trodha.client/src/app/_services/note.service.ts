// trodha.client/src/app/_services/note.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUserNote, NoteImage, UpdateUserNote, UserNote } from '../_models/user-note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private apiUrl = 'http://localhost:5253/api/notes';

  constructor(private http: HttpClient) { }

  getNotes(): Observable<UserNote[]> {
    return this.http.get<UserNote[]>(this.apiUrl);
  }

  getNote(id: number): Observable<UserNote> {
    return this.http.get<UserNote>(`${this.apiUrl}/${id}`);
  }

  createNote(note: CreateUserNote): Observable<UserNote> {
    return this.http.post<UserNote>(this.apiUrl, note);
  }

  updateNote(id: number, note: UpdateUserNote): Observable<UserNote> {
    return this.http.put<UserNote>(`${this.apiUrl}/${id}`, note);
  }

  deleteNote(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getNoteImages(noteId: number): Observable<NoteImage[]> {
    return this.http.get<NoteImage[]>(`${this.apiUrl}/${noteId}/images`);
  }

  uploadImage(noteId: number, file: File): Observable<NoteImage> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<NoteImage>(`${this.apiUrl}/${noteId}/images`, formData);
  }

  deleteImage(imageId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/images/${imageId}`);
  }
}
