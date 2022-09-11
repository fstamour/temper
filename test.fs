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

variable /test-dir
create test-dir

here
sourcefilename
ndup char / find-char-from-end 
dup allot
nswap cmove

here
s" /test-outputs/" dup allot
nswap cmove

here test-dir - /test-dir !

: test-dir' test-dir /test-dir @ ;

: test-dir/
  { dest c-addr u }
  test-dir' dest swap cmove
  c-addr dest /test-dir @ + u cmove
  dest /test-dir @ u + ;

test-dir' 
n( % 111 110 000 )
mkdir-parents

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


