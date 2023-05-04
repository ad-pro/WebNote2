#!/bin/bash

host="https://localhost:7188"

NoteBookId=$1
NoteId=$2


echo "Delete Page at Notebook: $NoteBookId  NoteId $NoteId"

curl -X DELETE \
    --insecure \
    $host/notebooks/$NoteBookId/notes/$NoteId
