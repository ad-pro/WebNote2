#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

NoteBookId="nb1";
NoteId="note2"

${DIR}/deleteNote.sh "$NoteBookId" "$NoteId"

