\ buffer to store the layout string
\ that is currently being processed.
\ to help keep things simpler, we
\ store the layout stripped of
\ spaces
create layout #16 chars allot

#16 constant /layout

\ initialize layout
: 0layout
  layout /layout chars erase ;

\ push layout addr and its size
: layout' layout /layout ;

\ store a layout, stripping spaces
: @layout
  0layout
  layout swap remove-spaces drop ;

\ print the stored layout
: .layout layout' type cr ;

: test-@layout
  s" oooo oord dddd rrrr"
  @layout
  .s
  layout' type ;