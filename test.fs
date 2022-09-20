include avr.fs


: test-find-char-from-end
  s" abcd" [char] c
  find-char-from-end assert( 2 = )

  s" abcd" [char] f
  find-char-from-end assert( -1 = )
  ;
test-find-char-from-end


: test-1st-bit
  cr
  0 0 1st-bit .
  #2 1 1st-bit .
  #8 1 1st-bit .
  %10111 0 1st-bit .
  -1 0 1st-bit . ;


(
%10110 trim-0s .s
%111 trim-0s .s
)

(
%10110 trim-1s .s 2drop
%111 trim-1s .s
)

(
%1000111100 #2 #4
clear-run .ss
)

(
%1000111100 leftmost-run
.s
\ 2 4
)

(
: frob
  [ %1000111100 spread ] ;
see frob

$ffff frob cr .ss cr drop
#42 frob cr .ss cr drop
#43 frob cr .ss cr drop
#21 frob cr .ss cr drop
)


: test->spread
 s" aabbaa"
 [char] a
 %1101
 >spread
 dup . cr
 assert( %110001 = ) ;

test->spread

: test->mask
  s" oooo oord dddd rrrr"
  @layout
  layout' 2dup 2dup
  [char] o .c >mask dup .2b cr
  assert( %1111110000000000 = )
  [char] d .c >mask dup .2b cr
  assert( %111110000 = )
  [char] r .c >mask dup .2b cr
  assert( %1000001111 = ) ;

\ ==========================

test-@layout cr cr
test->mask

(
s" oooo oord dddd rrrr"
@layout
layout' char o bl replace-char
.layout
layout' next-operand .
layout' char r bl replace-char
layout' next-operand .
layout' char d bl replace-char
layout' next-operand .
quit
)


: test-disassembly
  +compile-mode
  10 0assembly
  ret, clc,
  #7 #12 adc,
  decompile
  \ free-assembly
  ;

.instructions
test-disassembly

\ ==========================

\ we want to have some tests that write
\ files, so we compute a directory path
\ relative to the source files

\ the size of the path
variable /test-dir
\ the path
create test-dir

here
sourcefilename
ndup char / find-char-from-end
\ TODO this won't work if we put source
\ in a subdirectory of the current working
\ directory (I think)
dup -1 = [if]
  \ no slash found, so it's just a filename
  2drop
  unused
  get-dir
  allot
[else]
  dup allot
  nswap cmove
[then]


\ we append the name of the directory
here
s" /test-outputs/" dup allot
nswap cmove

\ we save the length of the path
here test-dir - /test-dir !

: test-dir' test-dir /test-dir @ ;

: test-dir/
  { dest c-addr u }
  test-dir' dest swap cmove
  c-addr dest /test-dir @ + u cmove
  dest /test-dir @ u + ;

test-dir'
n( % 111 110 000 ) \ permission flags
mkdir-parents
\ skip "already exists" error
dup -529 <> [if] throw [else] drop [then]

pad s" output.bin" test-dir/
write-assembly

\ ==========================

(
' create @instruction
' + @instruction
cr
.s cr
instructions 24 dump cr
.instructions
quit
)
