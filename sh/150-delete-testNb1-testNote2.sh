#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

NoteBookId="testNb1";
NoteId="testNote2"

${DIR}/deleteNote.sh "$NoteBookId" "$NoteId"

