\ defining word for instructions with
\ 0 operands (they're basically
\ constants)

: i0-doc ( a-addr - )
  doc-mode?
  if
    str@
    r> drop exit
  then ;

: i0-asm ( a-addr - )
  compile-mode?
  if 
    str+ 2b@ @assembly
    r> drop exit
  then ;

: i0-disassembly ( u a-addr )
  decompile-mode?
  if
    str+ 2b@ =
    r> drop exit
  then ;

: i0 ( c-addr u u - )
  create 
    -rot
    str, \ store the docstring
    2b, \ store the machine code
    \ register the current word into
    \ the list of instructions
    latestxt @instruction
  does>
    i0-doc
    i0-asm
    i0-disassembly
    false abort" Invalid mode" ;