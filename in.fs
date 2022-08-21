\ defining word for instructions
\ with N operands

(
- In this file, I'm trying to generate
  words from very high-level
  specifications.
- I call these specification 
  "layouts"
- This is an example layout:
  s" oooo oord dddd rrrr"
- The layout is stored without
  space. This simplify things.
- From a layout, we extract
  "operands"
- an operand is represented by a
  char
- a list of deduplicated operands
  is stored
- For each operand, we compute a
  mask that is used at
  compile-time
- masks are 16 bits
- there are two places where 
  masks are used:
  - at compile-time
  - at run-time
- the compile-time masks are used
  to compute the masks that are
  going to be used at run-time
- the masks used at run-time each
  come with a corresponding
  shift
- only one list of masks and shifts
  is stored at a time, for one
  operand
)


( TODOs
- "shifts" should be called "offsets"
- "masks+shifts" should be called "runs"
- "il" should be called "in"
- review the doc at the top
- find a better name than "process layout" and ":layout"
- for each runs, we should store the length and offset instead of a mask and an offset
)

include in-layout.fs
include in-operands.fs
include in-masks.fs

\ ==========================

\ Process a layout:
\ given a layout
\ - store the layout, without spaces
\ - store the unique operands
\ - compute a mask for each
\   operands
: :layout ( c-addr u )
  0layout
  0operands
  0masks
  @layout
  layout>operands
  #operand @ 0 
  do
    operands i th-c@
    operand+layout>mask
  loop ;

: test-:layout
  cr
  ." input: " s" oooo oord dddd rrrr"
  2dup type cr
  :layout
  ." spaces removed: " .layout
  ." unique operands: " 
  .operands cr
  ." masks: "
  .masks ;

\ ==========================

\ convert mask into a pair of mask
\ and shift, only for the least 
\ significant run of 1s
: >mask+shift ( u1 - u2 mask shift )
  trim-0s
  swap
  trim-1s
  1s
  rot ;

\ %1000011110 >mask+shift cr .s
\ %100001111 >mask+shift cr .s

\ store a list of pairs of mask and
\ shifts
create masks+shifts 
  #32 cells allot
variable #masks+shifts

\ initialize masks+shifts
: 0masks+shifts
  0 #masks+shifts !
  masks+shifts #32 cells erase ;

\ compute the index of the nth
\ mask
: mask+ ( u - u ) 2* cells ;

\ compute the index of the nth
\ shift
: shift+ 2* 1+ cells ;

\ compute the pointer to the latest
\ mask
: >mask
  masks+shifts
  #masks+shifts @ mask+
  + ;

\ compute the pointer to the latest
\ shift
: >shift
  masks+shifts
  #masks+shifts @ shift+ 
  + ;

\ store a pair of mask and shift
: @mask+shift
  swap >mask ! >shift !
  #masks+shifts 1+! ;

: (.masks+shifts) ( c-addr n - )
  cr
  0 ?do
    ." mask: " 
    dup i mask+ + @ 
    .2b
    ." shift: "
    dup i shift+ + ?
    cr
  loop
  drop ;

\ prints all stored pairs of masks
\ and shifts
: .masks+shifts ( - )
   masks+shifts
   #masks+shifts @
   (.masks+shifts) ;

(
%100001111 
  >mask+shift @mask+shift
  >mask+shift @mask+shift
  drop
.masks+shifts
)

\ convert an operand's mask into
\ a list of pairs of mask and shift 
\ that can be used to spread the
\ bits of an operands at the right
\ places.
: mask>masks+shifts
 0masks+shifts
 begin
 dup while
  >mask+shift @mask+shift
 repeat
 drop ;

(
%100001111 
mask>masks+shifts
.masks+shifts
)

(
\ r's mask
$20F mask>masks+shifts
.masks+shifts
)

: masks+shifts, ( - )
  masks+shifts #masks+shifts @
  2* str, ;

\ test->layout cr cr
\ test-operand+layout>mask
\ test-operand
\ test-layout>operands
test-:layout

cr
char r mask@
mask>masks+shifts
.masks+shifts

(
here  cell+ dup
#masks+shifts @ 2* cells

masks+shifts,

." number of masks and shifts: "
swap -1 cells + ?
dump'
)

\ take the value of an operand
\ take the address of the counted
\ list of pairs (of masks and shifts)
\ spread the bits of the operand
: encode-operand
  ( u1 a-addr - u2 )
  {: x body :}
  0
  body @ 0
  ?do
    \ you can see from the
    \ comments that this was trivial
    body cell+ dup 
    \ ." a body body : ".ss
    i mask+ + @ dup
    \ ." a body m m: " .ss
    rot i shift+ + @
    swap
    \ ." a m s m: " .ss
    \ ." x: " x . cr
    x and
    \ ." a m s xm: " .ss
    swap lshift 
    \ ." a m xms :" .ss
    swap log2 \ size of the mask
    x swap rshift to x
    \ ." x: " x . cr
    or
    \ ." acc: " dup . cr
  loop ;

(
cr
here
masks+shifts,
#31 swap encode-operand
.ss
)

\ ===========================
\ I.nstruction defined from L.ayout
\ il

\ get the docstring
: il-doc@ ( body - c-addr u ) str@ ;

\ get the address of the opcode
: il-opcode ( body - c-addr ) str+ ;

\ get the address of the layout
: il-layout ( body - c-addr )
  il-opcode cell+ ;

\ get the layout string
: il-layout@ ( body - c-addr u )
  il-layout str@ ;

\ get the address of the operands
\ (it's also a cell-counted string )
: il-operands ( body - c-addr )
  il-layout str+ ;

\ get the number of operands
: il-#operands@ ( body - u )
  il-operands @ ;

\ get the nth operand
: il-operand@ ( body n - c )
  chars
  swap il-operands cell+
  + c@ ;

\ get the operands string
: il-operands@ ( body - c-addr u )
  il-operands str@ ;

\ get the address of the list of
\ masks
: il-masks ( body - c-addr )
  il-operands str+ ;

\ get the nth mask
: il-mask@ ( body n - u )
  cells swap il-masks + @ ;

\ get the address of the (list of 
\ (lists of (pairs of (mask and
\ shift))))
: il-masks+shifts
  ( body - c-addr )
  il-masks il-#operands@ cells + ;

\ ===========================

: .il-masks ( body - )
  \ todo
  ." masks: " cr
  dup il-#operands@ 0
  do
    dup i il-operand@ emit
    dup i il-mask@ .2b cr
  loop
  drop ;

: .il-masks+shifts ( body - )
  \ todo
  ." masks and shifts: "
  dup il-#operands@ 0
  do
    \ TODO that's where I'm at
    \ il-masks+shifts
    \ (.masks+shifts)
  loop
  drop ;

\ print all opcode's info
\ for debugging
: .il ( "name" - )
  ' dup
  name>string h1
  >body
  ." docstring: " dup il-doc@ type cr
  ." opcode: " dup il-opcode @ . cr
  ." layout: " dup il-layout@ type cr
  ." operands: " 
  dup il-operands@ type cr
  dup .il-masks cr
  .il-masks+shifts cr ;
  
\ ===========================

: operands, ( - )
  operands #operand @ str, ;

: masks, ( - )
  #operand @ 0
  do
    i operands@ mask@ ,
  loop ;

: all-masks+shifts, ( - )
  #operand @ 0
  do
    \ currently its
    \ size list size list etc
    \ I want size size size list list list instead 
    i operands@ mask@ \ get
    mask>masks+shifts \ transform 
    masks+shifts, \ store
  loop ;

: il-create
  ( u c-addr u c-addr-u "name" )
  ( opcode layout docstring name )
  create
  str, \ store the docstring
  , \ store the opcode
  2dup
  str, \ store the raw layout
  :layout \ process the layout
  operands,
  masks,
  all-masks+shifts, ;

: il-doc ( a-addr - )
  doc-mode?
  if
    il-doc@ type
    \ Todo print the operands'
    \ values
    r> drop exit
  then ;

: il-asm ( a-addr - )
  compile-mode?
  if
    $ffff
    \ todo
    @assembly
    r> drop exit
  then ;

: il-disassembly ( u a-addr )
  decompile-mode?
  if
    false 
    \ todo
    \ str+ 2b@ =
    \ Todo print the operands'
    \ values
    r> drop exit
  then ;

: il
  ( u c-addr u c-addr-u "name" )
  ( opcode layout docstring name )
  il-create
  \ register the current word to the
  \ list of instructions
  latestxt @instruction
  does>
    il-doc
    il-asm
    il-disassembly
    false abort" Invalid mode" ;

\ ===========================

s" oooo oord dddd rrrr"
$3
s" Add with Carry"
il adc,

cr
.il adc,
