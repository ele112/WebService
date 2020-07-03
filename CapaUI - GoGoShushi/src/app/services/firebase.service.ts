import { Injectable } from '@angular/core';

import { AngularFireStorage, AngularFireUploadTask, AngularFireStorageReference } from '@angular/fire/storage';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { resolve } from 'url';


@Injectable()
export class FirebaseService {
    public nombreArchivo = '';
    public fileRef: Observable<any>;
    public taskUpload: Observable<number>;
    constructor(
        private storage: AngularFireStorage,
        private database: AngularFirestore) { }



    public subirImagen(event: FileList) {

        const n = Math.floor(Math.random() * 1000);
        const file = event.item(0);

        if (file.type.split('/')[0] !== 'image') { 
         console.error('unsupported file type :( ')
         return;
        }
        this.nombreArchivo = 'image_'+n;
        const fileRef = this.storage.ref('photos/image_'+n);
        const task = this.tareaCloudStorage('photos/image_'+n, file);

        this.taskUpload = task.percentageChanges();
        this.fileRef = fileRef.getDownloadURL();
    }




    //Tarea para subir archivo
    private tareaCloudStorage(nombreArchivo: string, datos: any): AngularFireUploadTask {
        return this.storage.upload(nombreArchivo, datos);
    }

    //Referencia del archivo
    private referenciaCloudStorage(nombreArchivo: string): AngularFireStorageReference {
        return this.storage.ref(nombreArchivo);
    }


}