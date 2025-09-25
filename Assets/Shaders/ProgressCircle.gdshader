shader_type canvas_item;

uniform float Progress : hint_range(0.0, 1.0);
uniform float CenterDistance : hint_range(0.0, 1.0) = 0.5;
uniform float ProgressStart : hint_range(0.0, 1.0) = 0.75;
uniform float ProgressEnd : hint_range(0.0, 1.0) = 1.0;
uniform vec4 CenterColor : source_color = vec4(1.0);
uniform vec4 InProgress : source_color = vec4(0.0,1.0,0.0,1.0);
uniform vec4 DoneProgress : source_color = vec4(1.0,0.0,0.0,1.0);

vec2 polar_coordinates(vec2 uv)
{
	float radius = length(uv);
	float angle = atan(uv.x, uv.y);

	return vec2(angle / TAU, radius);
}

void fragment() 
{
	vec2 centeredUV = (UV.xy * 2.0) - 1.0;
    vec2 sample = polar_coordinates(centeredUV);
    
    vec4 resultColor = vec4(0.0);
    
    if (sample.y < CenterDistance)
    {
        resultColor = CenterColor;
    }
    else if (sample.y > ProgressStart && sample.y < ProgressEnd)
    {
        resultColor = Progress > (sample.x + 0.5) ? DoneProgress : InProgress;
    }
    
    COLOR = resultColor;
}