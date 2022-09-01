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
