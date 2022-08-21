\ the assembly words will behave
\ differently based on the current 
\ mode

0 constant compile-mode
1 constant doc-mode
2 constant decompile-mode

variable mode

: mode@ mode @ ; \ *shrug*
: @mode mode ! ;

: +compile-mode ( - )
  compile-mode @mode ;
: +doc-mode ( - )
  doc-mode @mode ;
: +decompile-mode ( - )
  decompile-mode @mode ;

: compile-mode? ( - f )
  mode@ compile-mode = ;
: doc-mode? ( - f )
  mode@ doc-mode = ;
: decompile-mode? ( - f )
  mode@ decompile-mode = ;
  
\ ===========================
  
\ run an xt in doc-mode
: idoc-exec ( xt - )
  mode@ >r
  +doc-mode
  execute
  r> @mode ;

\ Print an instruction's
\ documentation 
: .idoc ( "name" - )
  ' idoc-exec cr type ;

: decode-assembly ( u - xt|f )
  {: code :}
  \ this code assumes
  \ decompile-mode? is true
  #instruction @ 0 ?do
    instructions i cells + @
    code over execute
    if
      unloop exit
    then
    drop
  loop
  false ;

: decompile ( - )
  s" Disassembly " h1
  mode@ >r
  +decompile-mode
  #assembly @ 0
  do
    i assembly@
    dup .
    decode-assembly
    dup
    if
      dup
      .xt-name ."   \ " idoc-exec type
    else
      drop
    then
    cr
  loop
  r> @mode ;